import { Moon } from "@styled-icons/heroicons-outline";
import { Checkbox } from "../../complib/inputs/checkbox/Checkbox";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { usePrefersTheme } from "../../hooks/usePrefersTheme";
import { Logo } from "../common/Logo";
import { UserMenu } from "./components/UserMenu";
import { toggleDarkTheme } from "./components/UserMenu.helpers";
import { UserMenuLabel } from "./components/UserMenu.styled";
import { HeaderContainer } from "./Header.styles";

export const Header = () => {
  const { data, isLoading } = useGetCurrentUser();
  const [colorTheme] = usePrefersTheme("light");

  return (
    <HeaderContainer>
      <Logo height={"100%"} width={"fit-content"} alt="" />
      {!isLoading && (
        <UserMenu name={`${data?.firstName} ${data?.lastName}`}>
          <UserMenuLabel>
            <Checkbox checked={colorTheme == "dark"} onClick={() => toggleDarkTheme()} />
            Enable dark mode
            <Moon size={20} />
          </UserMenuLabel>
        </UserMenu>
      )}
    </HeaderContainer>
  );
};
