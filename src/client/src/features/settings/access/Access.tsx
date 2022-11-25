import { mapMimirorgUserCmToUserItem } from "common/utils/mappers/mapMimirorgUserCmToUserItem";
import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useGetPendingUsers } from "external/sources/company/company.queries";
import { AccessPlaceholder } from "features/settings/access/placeholder/AccessPlaceholder";
import { PermissionCard } from "features/settings/common/permission-card/PermissionCard";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const Access = () => {
  const theme = useTheme();
  const { t } = useTranslation();
  const pendingUsersQuery = useGetPendingUsers();

  const users = pendingUsersQuery.data ?? [];
  const showPlaceholder = users && users.length === 0;

  return (
    <SettingsSection title={t("settings.access.title")}>
      <Text variant={"title-medium"} mb={theme.tyle.spacing.l}>
        {t("settings.access.users")}
      </Text>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        {showPlaceholder && <AccessPlaceholder text={t("settings.access.placeholders.users")} />}
        {users.map((x, i) => (
          <PermissionCard key={i} user={mapMimirorgUserCmToUserItem(x)} />
        ))}
      </Flexbox>
    </SettingsSection>
  );
};
