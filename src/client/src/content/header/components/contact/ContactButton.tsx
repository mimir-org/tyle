import { MimirorgCompanyCm, MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Mail } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Select } from "../../../../complib/inputs";
import { Box } from "../../../../complib/layouts";
import { Dialog } from "../../../../complib/overlays";
import { Text } from "../../../../complib/text";
import { useGetFilteredCompanies } from "../../../../hooks/useGetFilteredCompanies";
import { UserMenuButton } from "../menu/UserMenuButton";
import { ContactCard } from "./ContactCard";

/**
 * Component that displays a button with a dialog for finding contact information about
 * the various organizations that the user has permissions to view.
 *
 * @constructor
 */
export const ContactButton = () => {
  const { t } = useTranslation("translation", { keyPrefix: "user.menu.contact" });
  const [company, setCompany] = useState<MimirorgCompanyCm | null>();
  const companies = useGetFilteredCompanies(MimirorgPermission.Read);

  const manager = company?.manager;
  const managerName = `${company?.manager?.firstName} ${company?.manager?.lastName}`;
  const managerEmail = company?.manager?.email;

  const showManager = company && manager;
  const showNotFound = company && !manager;

  return (
    <Dialog
      title={t("title")}
      description={t("description")}
      width={"500px"}
      content={
        <>
          <Select
            placeholder={t("select")}
            options={companies}
            getOptionLabel={(x) => x.name}
            onChange={(x) => setCompany(x)}
            value={companies.find((x) => x.id === company?.id)}
          />

          <Box display={"flex"} alignItems={"center"} minHeight={"70px"}>
            {showManager && <ContactCard name={managerName} email={managerEmail} />}
            {showNotFound && <Text>{t("notFound")}</Text>}
          </Box>
        </>
      }
    >
      <UserMenuButton icon={<Mail size={24} />}>{t("title")}</UserMenuButton>
    </Dialog>
  );
};
