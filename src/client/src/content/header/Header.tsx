import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useTheme } from "styled-components";
import { Flexbox } from "../../complib/layouts";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { useGetFilteredCompanies } from "../../hooks/filter-companies/useGetFilteredCompanies";
import { Logo } from "../common/logo";
import { ContactButton } from "./components/contact/ContactButton";
import { UserInfo } from "./components/info/UserInfo";
import { LogoutButton } from "./components/logout/LogoutButton";
import { UserMenu } from "./components/menu/UserMenu";
import { ThemeButton } from "./components/theme/ThemeButton";
import { mapPermissionDescriptions } from "./Header.helpers";
import { HeaderContainer } from "./Header.styles";

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
