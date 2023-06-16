/* eslint-disable @typescript-eslint/no-empty-function */
import {Avatar, Group, Header, Loader, Text, TextInput, Title} from "@mantine/core";
import {IconSearch} from "@tabler/icons-react";
import {useDebouncedState} from "@mantine/hooks";
import useAsyncEffect from "use-async-effect";
import axios from "axios";
import {useState} from "react";
import {useAuth} from "react-oidc-context";
import {useNavigate} from "react-router-dom";
import {spotlight, SpotlightAction, SpotlightProvider} from "@mantine/spotlight";

export default function PageHeader({
                                       onSearchResult = () => {
                                       }, useSpotlight = false
                                   }: {
    onSearchResult?: (data: any) => void;
    useSpotlight?: boolean
}) {
    const auth = useAuth();
    const navigation = useNavigate();
    const [showSearchLoader, setShowSearchLoader] = useState(false);
    const [searchValue, setSearchValue] = useDebouncedState('', 500);
    const [spotlightItems, setSpotlightItems] = useState<SpotlightAction[]>([]);

    useAsyncEffect(async () => {
        setShowSearchLoader(true);
        const response = await axios.get(`/parties?search=${searchValue}`);
        onSearchResult(response.data)

        if (useSpotlight) {
            setSpotlightItems([]);

            setSpotlightItems(
                response.data.results.map((v: any) => {
                    return {
                        title: v.name,
                        description: `Lijstpositie ${v.positionNr}`,
                        // Does not reload page on redirect
                        onTrigger: () => navigation(`/${v.slug}`),
                    }
                }));
        }

        setShowSearchLoader(false);

    }, [searchValue])

    const onClick = (ev: any) => {
        if (!useSpotlight) return;

        spotlight.open();
        ev.currentTarget.blur();
    }

    return (
        <Header p="xs" mb="md" height="100%">
            <SpotlightProvider
                searchIcon={<IconSearch size="1.2rem"/>}
                searchPlaceholder="Zoek naar een partij..."
                shortcut="/"
                actions={spotlightItems}
                nothingFoundMessage="Begin met typen..."
                onQueryChange={(value) => setSearchValue(value)}
            />

            <Group sx={{height: '100%'}} px={20} position="apart">
                <Title fw={700} size="h3" onClick={() => navigation("/")}>
                    Digi
                    <Text span c="cyan">Choice</Text>
                </Title>
                <TextInput placeholder="Zoek naar een partij..."
                           rightSection={showSearchLoader ? <Loader size="xs"/> : <IconSearch size="50%"/>}
                           onClick={onClick}
                           readOnly={useSpotlight}
                           onChange={(ev: any) => setSearchValue(ev.currentTarget.value)}
                           style={{flex: 1}} mx={"25%"}/>

                {auth.isAuthenticated ?
                    <Avatar size="md" variant="filled" onClick={() => auth.signoutRedirect()}>
                        {(`${auth.user?.profile?.given_name?.charAt(0)}${auth.user?.profile?.family_name?.charAt(0)}`)}
                    </Avatar>
                    :
                    <Avatar size="md" variant="filled" onClick={() => auth.signinRedirect()}></Avatar>
                }
            </Group>


        </Header>
    )
}