import { Flexbox, Text } from "@mimirorg/component-library";
import SettingsSection from "components/SettingsSection";
import { useTheme } from "styled-components";

const Access = () => {
  const theme = useTheme();
  //const pendingUsersQuery = useGetPendingUsers();

  //const users = pendingUsersQuery.data?.sort((a, b) => a.firstName.localeCompare(b.firstName)) ?? [];
  //const showPlaceholder = users && users.length === 0;

  return (
    <SettingsSection title="Access">
      <Text variant={"title-medium"} spacing={{ mb: theme.mimirorg.spacing.l }}>
        New users
      </Text>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xxxl}>
        {/*showPlaceholder && <AccessPlaceholder text="No new users have requested access" />*/}
        {/*users.map((user) => (
          <RoleCard key={user.id} user={mapMimirorgUserCmToUserItem(user)} />
        ))*/}
      </Flexbox>
    </SettingsSection>
  );
};

export default Access;
