import router from "./routes.tsx";
import {RouterProvider} from "react-router-dom";
import {useEffect} from "react";
import {hasAuthParams, useAuth} from "react-oidc-context";


function App() {

    const auth = useAuth();
    // Causes user to silently signin when re-entering page
    useEffect(() => {

        if (hasAuthParams() && !auth.isAuthenticated) auth.signinSilent()
    }, []);

    return (
        <RouterProvider router={router}/>
    )
}

export default App
