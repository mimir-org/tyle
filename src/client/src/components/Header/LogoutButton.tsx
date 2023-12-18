import { ArrowLeftOnRectangle } from "@styled-icons/heroicons-outline";
import { useLogout } from "api/authenticate.queries";
import UserMenuButton from "./UserMenuButton";

const LogoutButton = () => {
  const mutation = useLogout();

  return (
    <UserMenuButton dangerousAction icon={<ArrowLeftOnRectangle size={24} />} onClick={() => mutation.mutate()}>
      Logout
    </UserMenuButton>
  );
};

export default LogoutButton;
