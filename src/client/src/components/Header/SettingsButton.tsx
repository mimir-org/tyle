import { PopoverClose } from "@radix-ui/react-popover";
import { Cog } from "@styled-icons/heroicons-outline";
import PlainLink from "components/PlainLink";
import { settingsBasePath } from "components/SettingsLayout/SettingsRoutes";
import UserMenuButton from "./UserMenuButton";

const SettingsButton = () => {
  return (
    <PopoverClose asChild>
      <PlainLink tabIndex={-1} to={settingsBasePath}>
        <UserMenuButton tabIndex={0} icon={<Cog size={24} />}>
          Settings
        </UserMenuButton>
      </PlainLink>
    </PopoverClose>
  );
};

export default SettingsButton;
