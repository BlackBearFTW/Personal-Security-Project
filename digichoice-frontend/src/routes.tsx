import {createBrowserRouter} from "react-router-dom";
import DetailPage from "./pages/DetailPage.tsx";
import OverviewPage from "./pages/OverviewPage.tsx";
import "./axiosSettings.ts"
import axios from "axios";

//const key = "oidc.user:http://localhost:8080/realms/digid:digi-choice"

const router = createBrowserRouter([
    {
        path: "/",
        element: <OverviewPage/>,
        loader: async () => {
            return await axios.get(`/parties`);
        },
    },
    {
        path: "/:partyName",
        element: <DetailPage/>,
        loader: async ({params}) => {
            return await axios.get(`/parties/${params.partyName}`);
        },
    },
]);

export default router;