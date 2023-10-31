import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "hooks/filter-companies/useGetFilteredCompanies";
import { Flexbox, Text } from "@mimirorg/component-library";
import { useGetCurrentUser } from "api/user.queries";
import { ContactButton } from "components/Header/ContactButton";
import { mapPermissionDescriptions } from "components/Header/Header.helpers";
import { HeaderContainer } from "components/Header/Header.styles";
import { HeaderHomeLink } from "components/Header/HeaderHomeLink";
import { LogoutButton } from "components/Header/LogoutButton";
import { FeedbackButton } from "components/Header/FeedbackButton";
import { SettingsButton } from "components/Header/SettingsButton";
import { ThemeButton } from "components/Header/ThemeButton";
import { UserInfo } from "components/Header/UserInfo";
import { UserMenu } from "components/Header/UserMenu";
import { useTheme } from "styled-components";
import config from "../../common/utils/config";

export const Header = () => {
  const theme = useTheme();
  const userQuery = useGetCurrentUser();

  const companies = useGetFilteredCompanies(MimirorgPermission.Read);
  const permissions = mapPermissionDescriptions(userQuery.data?.permissions ?? [], companies);

  const userInitials = `${userQuery.data?.firstName?.[0]}${userQuery.data?.lastName?.[0]}`;
  const userFullName = `${userQuery.data?.firstName} ${userQuery.data?.lastName}`;
  const userRoles = userQuery.data?.roles;

  return (
    <HeaderContainer>
      <HeaderHomeLink />
      {!userQuery.isLoading && (
        <UserMenu name={userInitials}>
          <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.base}>
            <UserInfo name={userFullName} permissions={permissions} roles={userRoles} />
            <ThemeButton />
            <ContactButton />
            <FeedbackButton />
            <SettingsButton />
            <LogoutButton />
            <Flexbox alignItems={"center"} justifyContent={"center"} gap={theme.mimirorg.spacing.base}>
              <Text style={{ color: "gray" }} variant={"body-small"}>
                {"Tyle version: " + config.TYLE_VERSION}
              </Text>
            </Flexbox>
          </Flexbox>
        </UserMenu>
      )}
    </HeaderContainer>
  );
};
