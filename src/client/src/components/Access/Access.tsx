import { Flexbox, Text } from "@mimirorg/component-library";
import { useGetPendingUsers } from "api/company.queries";
import { mapMimirorgUserCmToUserItem } from "common/utils/mappers/mapMimirorgUserCmToUserItem";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import PermissionCard from "../PermissionCard";
import SettingsSection from "../SettingsSection";
import AccessPlaceholder from "./AccessPlaceholder";

const Access = () => {
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const pendingUsersQuery = useGetPendingUsers();

  const users = pendingUsersQuery.data?.sort((a, b) => a.firstName.localeCompare(b.firstName)) ?? [];
  const showPlaceholder = users && users.length === 0;

  return (
    <SettingsSection title={t("access.title")}>
      <Text variant={"title-medium"} spacing={{ mb: theme.mimirorg.spacing.l }}>
        {t("access.users")}
      </Text>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxxl}>
        {showPlaceholder && <AccessPlaceholder text={t("access.placeholders.users")} />}
        {users.map((user) => (
          <PermissionCard key={user.id} user={mapMimirorgUserCmToUserItem(user)} />
        ))}
      </Flexbox>
    </SettingsSection>
  );
};

export default Access;
