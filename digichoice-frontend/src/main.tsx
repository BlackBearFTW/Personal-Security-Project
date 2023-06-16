import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import {AuthProvider} from "react-oidc-context";
import {MantineProvider} from "@mantine/core";
import {Notifications} from "@mantine/notifications";


ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <AuthProvider
            authority="http://localhost:8080/realms/digid"
            client_id="digi-choice"
            redirect_uri="http://localhost:5173/"
            scope="openid profile"
            automaticSilentRenew={true}

            onSigninCallback={async () => {
                // Remove query string obtained from callback url
                window.history.replaceState(null, '', window.location.pathname);
            }}
        >
            <MantineProvider withGlobalStyles withNormalizeCSS theme={{colorScheme: 'dark'}}>
                <Notifications/>
                <App/>
            </MantineProvider>
        </AuthProvider>
    </React.StrictMode>,
)
