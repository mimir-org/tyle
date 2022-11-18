import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useGetCurrentUser, useGetPendingUsers } from "external/sources/user/user.queries";
import { AccessCard } from "features/settings/access/card/AccessCard";
import { AccessPlaceholder } from "features/settings/access/placeholder/AccessPlaceholder";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const Access = () => {
  const theme = useTheme();
  const { t } = useTranslation();
  const currentUserQuery = useGetCurrentUser();
  const currentUserCompany = currentUserQuery.data?.companyId;
  const pendingUsersQuery = useGetPendingUsers(String(currentUserCompany));

  const users = pendingUsersQuery.data ?? [];
  const showPlaceholder = users && users.length === 0;

  return (
    <SettingsSection title={t("settings.access.title")}>
      <Text variant={"title-medium"} mt={theme.tyle.spacing.xxxl} mb={theme.tyle.spacing.l}>
        {t("settings.access.users")}
      </Text>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        {showPlaceholder && <AccessPlaceholder text={t("settings.access.placeholders.users")} />}
        {users.map((x, i) => (
          <AccessCard key={i} user={x} />
        ))}
      </Flexbox>
    </SettingsSection>
  );
};
