import React from "react";
import { Route, Routes } from "react-router-dom";
import BiletVarmi from "./biletVarmi";

const App = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<BiletVarmi />} />
                <Route path="/biletvarmi" element={<BiletVarmi />} />
            </Routes>
        </>
    );
};

export default App;
