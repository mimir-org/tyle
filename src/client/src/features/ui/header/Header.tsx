import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { Flexbox, Text } from "@mimirorg/component-library";
import { useGetCurrentUser } from "external/sources/user/user.queries";
import { ContactButton } from "features/ui/header/contact/ContactButton";
import { mapPermissionDescriptions } from "features/ui/header/Header.helpers";
import { HeaderContainer } from "features/ui/header/Header.styles";
import { HeaderHomeLink } from "features/ui/header/home-link/HeaderHomeLink";
import { LogoutButton } from "features/ui/header/logout-button/LogoutButton";
import { FeedbackButton } from "features/ui/header/feedback-button/FeedbackButton";
import { SettingsButton } from "features/ui/header/settings-button/SettingsButton";
import { ThemeButton } from "features/ui/header/theme-button/ThemeButton";
import { UserInfo } from "features/ui/header/user-info/UserInfo";
import { UserMenu } from "features/ui/header/user-menu/UserMenu";
import { useTheme } from "styled-components";
import config from "../../../common/utils/config";

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
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
            <UserInfo name={userFullName} permissions={permissions} roles={userRoles} />
            <ThemeButton />
            <ContactButton />
            <FeedbackButton />
            <SettingsButton />
            <LogoutButton />
            <Flexbox alignItems={"center"} justifyContent={"center"} gap={theme.tyle.spacing.base}>
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
