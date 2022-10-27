import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { Flexbox } from "complib/layouts";
import { useTheme } from "styled-components";
import { Logo } from "../../../common/components/logo";
import { useGetCurrentUser } from "../../../data/queries/auth/queriesUser";
import { ContactButton } from "./contact/ContactButton";
import { mapPermissionDescriptions } from "./Header.helpers";
import { HeaderContainer } from "./Header.styles";
import { LogoutButton } from "./logout-button/LogoutButton";
import { ThemeButton } from "./theme-button/ThemeButton";
import { UserInfo } from "./user-info/UserInfo";
import { UserMenu } from "./user-menu/UserMenu";

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
      <Logo height={"100%"} width={"fit-content"} alt="" />
      {!userQuery.isLoading && (
        <UserMenu name={userInitials}>
          <UserInfo name={userFullName} permissions={permissions} roles={userRoles} />
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
            <ThemeButton />
            <ContactButton />
            <LogoutButton />
          </Flexbox>
        </UserMenu>
      )}
    </HeaderContainer>
  );
};
