import { Box, Dialog, Select, Text } from "@mimirorg/component-library";
import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Envelope } from "@styled-icons/heroicons-outline";
import { useGetCompany } from "api/company.queries";
import { useGetFilteredCompanies } from "hooks/useGetFilteredCompanies";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import ContactCard from "./ContactCard";
import UserMenuButton from "./UserMenuButton";

/**
 * Component that displays a button with a dialog for finding contact information about
 * the various organizations that the user has permissions to view.
 *
 * @constructor
 */
const ContactButton = () => {
  const { t } = useTranslation("ui");
  const [selected, setSelected] = useState<number>();

  const companies = useGetFilteredCompanies(MimirorgPermission.Read);
  const { data: company } = useGetCompany(selected);

  const manager = company?.manager;
  const managerName = `${company?.manager?.firstName} ${company?.manager?.lastName}`;
  const managerEmail = company?.manager?.email;

  const showManager = company && manager;
  const showNotFound = company && !manager;

  return (
    <Dialog
      title={t("header.menu.contact.title")}
      description={t("header.menu.contact.description")}
      width={"500px"}
      content={
        <>
          <Select
            placeholder={t("header.menu.contact.select")}
            options={companies}
            getOptionLabel={(x) => x.name}
            onChange={(x) => setSelected(x?.id)}
            value={companies.find((x) => x.id === selected)}
          />

          <Box display={"flex"} alignItems={"center"} minHeight={"70px"}>
            {showManager && <ContactCard name={managerName} email={managerEmail} />}
            {showNotFound && <Text>{t("header.menu.contact.notFound")}</Text>}
          </Box>
        </>
      }
    >
      {companies.length > 0 && (
        <UserMenuButton icon={<Envelope size={24} />}>{t("header.menu.contact.title")}</UserMenuButton>
      )}
    </Dialog>
  );
};

export default ContactButton;
