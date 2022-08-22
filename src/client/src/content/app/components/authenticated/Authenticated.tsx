import { useTranslation } from "react-i18next";
import { Route, Routes } from "react-router-dom";
import { NotFound } from "../../../common/NotFound";
import { Explore } from "../../../explore";
import { AttributeForm } from "../../../forms/attribute/AttributeForm";
import { NodeForm } from "../../../forms/node/NodeForm";
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
          <Route path={"/form/attribute/edit/:id"} element={<AttributeForm isEdit />} />
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
