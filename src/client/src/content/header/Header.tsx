import { LibraryIcon } from "../../assets/icons/modules";
import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Logo } from "./components/Logo";
import { User } from "./components/User";
import { HeaderContainer } from "./Header.styles";

export const Header = () => {
  const { data, isLoading } = useGetCurrentUser();

  return (
    <HeaderContainer>
      <Logo name={":TYLE"} icon={LibraryIcon} />
      {!isLoading && <User name={`${data?.firstName} ${data?.lastName}`} />}
    </HeaderContainer>
  );
};
