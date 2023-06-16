import {
    ActionIcon,
    Affix,
    Avatar,
    Button,
    Checkbox,
    Container,
    Flex,
    Modal,
    rem,
    Table,
    Text,
    Title,
    Transition
} from "@mantine/core";
import {IconArrowRight, IconArrowUp, IconCheck, IconX} from "@tabler/icons-react";
import {useDisclosure, useToggle, useWindowScroll} from "@mantine/hooks";
import {useState} from "react";
import PageHeader from "../components/PageHeader.tsx";
import {useLoaderData} from "react-router-dom";
import axios, {AxiosResponse} from "axios";
import {useAuth} from "react-oidc-context";
import {notifications} from "@mantine/notifications";

interface PartyMember {
    id: string,
    positionNr: number,
    initials: string,
    firstname: string,
    lastname: string,
    gender: string,
    residentCity: string
}

export default function DetailPage() {
    // useLoaderData works like useState, it will rerender the component when the stored value changes.
    // You cannot wrap it in useState, since it won't reset the state value when the loader data changed
    // Because of that it won't rerender the page, so just use the loader without useState
    const partyInformation = (useLoaderData() as AxiosResponse).data;
    
    // react-oidc-context
    const auth = useAuth();
    const [scroll, scrollTo] = useWindowScroll();
    const [selectedMember, setSelectedMember] = useState<PartyMember | null>(null);
    const [showOverlay, overlayHandler] = useDisclosure(false);
    const [checked, setChecked] = useToggle();
    const [disableAllButtons, setAllDisabled] = useToggle();

    const onClick = (memberId: string) => {
        if (!auth.isAuthenticated) return auth.signinRedirect({redirect_uri: window.location.href});
        overlayHandler.open();

        // partyInformation is from loader
        const partyMember = partyInformation.partyMembers
            .find((member: PartyMember) => member.id === memberId);

        if (!partyMember) return;

        setSelectedMember(partyMember);
    }

    const onSubmitClick = async (memberId: string) => {

        try {
            await axios.post("/votes", {
                partyMemberId: memberId
            }, {
                headers: {
                    Authorization: `Bearer ${auth.user?.access_token}`
                }
            });

            notifications.show({
                title: "Uw stem is toegevoegd.",
                message: "Dankuwel voor uw stem, binnenkort komt de uitslag!",
                icon: <IconCheck size="1.1rem"/>,
                color: "cyan"
            });

            setAllDisabled(true)

        } catch (e: any) {

            notifications.show({
                title: "Er is een fout ontstaan!",
                message: e.response.data.title,
                icon: <IconX size="1.1rem"/>,
                color: "red"
            });
        }

        overlayHandler.close();
    }

    const rows = partyInformation.partyMembers.map((p: PartyMember) => (
        <tr key={p.positionNr}>
            <td>{p.positionNr}</td>
            <td>
                <Text fw={700}>{p.lastname}</Text>
                {p.initials} ({p.firstname}) ({p.gender})
            </td>
            <td>
                <Button disabled={disableAllButtons}
                        color="cyan"
                        compact
                        rightIcon={<IconArrowRight/>}
                        variant="outline"
                        onClick={() => onClick(p.id)}
                >
                    Stemmen
                </Button>
            </td>
        </tr>
    ));

    return (
        <>
            <PageHeader useSpotlight={true}/>


            <Modal.Root
                opened={showOverlay}
                onClose={overlayHandler.close}
                fullScreen
                transitionProps={{transition: 'slide-up', duration: 180}}
            >
                <Modal.Overlay/>
                <Modal.Content>

                    <Flex direction="column" style={{height: "100%"}}>
                        <Modal.Header>
                            <Modal.Title>Stemproces afronden</Modal.Title>
                            <Modal.CloseButton/>
                        </Modal.Header>

                        <Modal.Body style={{height: "100%"}}>
                            <Flex
                                justify="center"
                                align="center"
                                direction="column"
                                style={{height: "100%", justifyContent: "space-between"}}
                            >
                                <Flex direction="column" align="center">
                                    <Title>Weet u zeker dat u wilt stemmen?</Title>
                                    <Text c="dimmed">Zodra u uw stem doorzet, kunt u deze niet meer veranderen.</Text>
                                </Flex>

                                <Flex direction="column" align="center">
                                    <Avatar size={120} radius={120}
                                            src={`https://localhost:44392/CDN/${partyInformation.slug}.jpg`}/>
                                    <Text fw={700} pt="1rem">
                                        {selectedMember?.lastname}, {selectedMember?.initials} ({selectedMember?.firstname})
                                        uit {selectedMember?.residentCity}
                                    </Text>
                                    <Text c="dimmed">
                                        {partyInformation.name}, Lijstpositie {selectedMember?.positionNr}
                                    </Text>
                                </Flex>

                                <Flex direction="column" gap="sm" mb="1rem">
                                    <Checkbox
                                        label="Ik begrijp dat mijn stem eenmalig en anoniem is."
                                        checked={checked}
                                        color="cyan"
                                        onChange={(event) => setChecked(event.currentTarget.checked)}
                                    />
                                    <Flex gap="sm">
                                        <Button
                                            color="cyan"
                                            px={5}
                                            disabled={!checked}
                                            fullWidth
                                            rightIcon={<IconArrowRight size={16}/>}
                                            onClick={() => selectedMember && onSubmitClick(selectedMember.id)}
                                        >
                                            Stem toevoegen
                                        </Button>
                                    </Flex>
                                </Flex>

                            </Flex>

                        </Modal.Body>
                    </Flex>

                </Modal.Content>

            </Modal.Root>


            <Container>
                <Flex
                    gap="md"
                    justify="flex-start"
                    align="center"
                    direction="row"
                    m={50}
                >
                    <Avatar size={120} radius={120} src={`https://localhost:44392/CDN/${partyInformation.slug}.jpg`}/>
                    <p>
                        {partyInformation.description}
                    </p>
                </Flex>

                <Table mb={20}>
                    <thead>
                    <tr>
                        <th>Positie</th>
                        <th>Naam</th>
                        <th>Stemproces</th>
                    </tr>
                    </thead>
                    <tbody>
                    {rows}
                    </tbody>
                </Table>
            </Container>

            <Affix position={{bottom: rem(20), right: rem(20)}}>
                <Transition transition="slide-up" mounted={scroll.y > 0}>
                    {(transitionStyles) => (
                        <ActionIcon p={rem(10)} color="cyan" size="xl" radius="xl" style={transitionStyles}
                                    variant="filled"
                                    onClick={() => scrollTo({y: 0})}>
                            <IconArrowUp size="80%"/>
                        </ActionIcon>
                    )}
                </Transition>
            </Affix>
        </>
    );
}