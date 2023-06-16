import {Avatar, Button, Paper, Text} from "@mantine/core";
import {useNavigate} from "react-router-dom";

export default function PartyCard({title, slug, positionNr}: {
    title: string,
    slug: string,
    positionNr: number
}) {

    const navigate = useNavigate();

    return (
        <Paper
            radius="md"
            withBorder
            p="lg"
            sx={(theme) => ({
                backgroundColor: theme.colorScheme === 'dark' ? theme.colors.dark[8] : theme.white,
            })}
        >
            <Avatar
                src={`https://localhost:44392/CDN/${slug}.jpg`}
                size={120} radius={120} mx="auto"/>
            <Text ta="center" fz="lg" weight={500} mt="md" truncate>
                {title}
            </Text>
            <Text ta="center" c="dimmed" fz="sm">
                {"Lijstpositie " + positionNr}
            </Text>

            <Button variant="default" fullWidth mt="md" onClick={() => navigate(`/${slug}`)}>
                Bekijk Partij
            </Button>
        </Paper>
        /*        <Card shadow="sm" padding="lg" radius="sm" withBorder>
                    <Card.Section>
                        <Image
                            src="https://images.unsplash.com/photo-1527004013197-933c4bb611b3?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=720&q=80"
                            alt="Norway"
                        />
                    </Card.Section>

                    <Button variant="filled" color="cyan" fullWidth mt="md" radius="md" onClick={onClick}>
                        Bekijk Partij
                    </Button>
                </Card>*/
    );
}
