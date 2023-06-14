import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Envelope } from "@styled-icons/heroicons-outline";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { Select } from "complib/inputs";
import { Box } from "complib/layouts";
import { Dialog } from "complib/overlays";
import { Text } from "complib/text";
import { useGetCompany } from "external/sources/company/company.queries";
import { ContactCard } from "features/ui/header/contact/ContactCard";
import { UserMenuButton } from "features/ui/header/user-menu/UserMenuButton";
import { useState } from "react";
import { useTranslation } from "react-i18next";

/**
 * Component that displays a button with a dialog for finding contact information about
 * the various organizations that the user has permissions to view.
 *
 * @constructor
 */
export const ContactButton = () => {
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
      <UserMenuButton icon={<Envelope size={24} />}>{t("header.menu.contact.title")}</UserMenuButton>
    </Dialog>
  );
};
