import {ActionIcon, Affix, Container, LoadingOverlay, rem, SimpleGrid, Transition} from "@mantine/core";
import {IconArrowUp} from "@tabler/icons-react";
import {useSetState, useWindowScroll} from "@mantine/hooks";
import {AxiosResponse} from "axios";
import PartyCard from "../components/PartyCard.tsx";
import {useLoaderData} from "react-router-dom";
import PageHeader from "../components/PageHeader.tsx";

export default function OverviewPage() {
    const [visibleParties, setVisibleParties] = useSetState((useLoaderData() as AxiosResponse).data);
    const [scroll, scrollTo] = useWindowScroll();

    return (
        <>
            <LoadingOverlay visible={visibleParties == null} overlayBlur={5}/>

            <PageHeader onSearchResult={(data) => setVisibleParties(data)}/>
            <Container>
                <SimpleGrid cols={3} mb={20}>
                    {visibleParties ? visibleParties.results.map((party: any, index: number) => <PartyCard
                        title={`${party.name}${party.abbreviation == null ? "" : ` (${party.abbreviation})`}`}
                        slug={party.slug}
                        positionNr={party.positionNr}
                        key={index}
                    />) : ""}
                </SimpleGrid>
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