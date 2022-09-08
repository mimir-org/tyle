import { useTranslation } from "react-i18next";
import { Route, Routes } from "react-router-dom";
import { NotFound } from "../../../common/NotFound";
import { Explore } from "../../../explore";
import { AttributeForm } from "../../../forms/attribute/AttributeForm";
import { InterfaceForm } from "../../../forms/interface/InterfaceForm";
import { NodeForm } from "../../../forms/node/NodeForm";
import { TerminalForm } from "../../../forms/terminal/TerminalForm";
import { TransportForm } from "../../../forms/transport/TransportForm";
import { Header } from "../../../header/Header";
import { AuthenticatedContainer, AuthenticatedContentContainer } from "./Authenticated.styled";

export const Authenticated = () => {
  const { t } = useTranslation("translation", { keyPrefix: "notFound" });

  return (
    <AuthenticatedContainer>
      <Header />
      <AuthenticatedContentContainer>
        <Routes>
          <Route path={"/"} element={<Explore />} />
          <Route path={"/form/node"} element={<NodeForm />} />
          <Route path={"/form/node/clone/:id"} element={<NodeForm />} />
          <Route path={"/form/node/edit/:id"} element={<NodeForm isEdit />} />
          <Route path={"/form/attribute"} element={<AttributeForm />} />
          <Route path={"/form/attribute/clone/:id"} element={<AttributeForm />} />
          <Route path={"/form/terminal"} element={<TerminalForm />} />
          <Route path={"/form/terminal/clone/:id"} element={<TerminalForm />} />
          <Route path={"/form/terminal/edit/:id"} element={<TerminalForm isEdit />} />
          <Route path={"/form/transport"} element={<TransportForm />} />
          <Route path={"/form/transport/clone/:id"} element={<TransportForm />} />
          <Route path={"/form/transport/edit/:id"} element={<TransportForm isEdit />} />
          <Route path={"/form/interface"} element={<InterfaceForm />} />
          <Route path={"/form/interface/clone/:id"} element={<InterfaceForm />} />
          <Route path={"/form/interface/edit/:id"} element={<InterfaceForm isEdit />} />
          <Route
            path={"*"}
            element={
              <NotFound
                title={t("title")}
                subtitle={t("subtitle")}
                status={t("status")}
                linkText={t("link")}
                linkPath={"/"}
              />
            }
          />
        </Routes>
      </AuthenticatedContentContainer>
    </AuthenticatedContainer>
  );
};
