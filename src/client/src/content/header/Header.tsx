import { useTheme } from "styled-components";
import { Flexbox } from "../../complib/layouts";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Logo } from "../common/logo";
import { LogoutButton } from "./components/LogoutButton";
import { ThemeButton } from "./components/ThemeButton";
import { UserMenu } from "./components/UserMenu";
import { HeaderContainer } from "./Header.styles";

export const Header = () => {
  const theme = useTheme();
  const userQuery = useGetCurrentUser();

  const userInitials = `${userQuery.data?.firstName?.[0]}${userQuery.data?.lastName?.[0]}`;

  return (
    <HeaderContainer>
      <Logo height={"100%"} width={"fit-content"} alt="" />
      {!userQuery.isLoading && (
        <UserMenu name={userInitials}>
          <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.base}>
            <ThemeButton />
            <LogoutButton />
          </Flexbox>
        </UserMenu>
      )}
    </HeaderContainer>
  );
};
