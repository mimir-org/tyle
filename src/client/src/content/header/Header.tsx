import { useGetCurrentUser } from "../../data/queries/auth/queriesUser";
import { Logo } from "../common/Logo";
import { User } from "./components/User";
import { HeaderContainer } from "./Header.styles";

export const Header = () => {
  const { data, isLoading } = useGetCurrentUser();

  return (
    <HeaderContainer>
      <Logo height={"100%"} width={"fit-content"} alt="" />
      {!isLoading && <User name={`${data?.firstName} ${data?.lastName}`} />}
    </HeaderContainer>
  );
};
