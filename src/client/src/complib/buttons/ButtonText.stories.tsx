import { ComponentMeta } from "@storybook/react";
import { Button } from "./Button";
import { LibraryIcon } from "../../assets/icons/modules";

export default {
  title: "Buttons/Text",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button variant={"text"}>Button</Button>;

export const Disabled = () => (
  <Button variant={"text"} disabled>
    Button
  </Button>
);

export const WithIconOnly = () => (
  <Button variant={"text"} icon={LibraryIcon} iconOnly>
    Hidden text
  </Button>
);

export const WithIconLeftAndText = () => (
  <Button variant={"text"} icon={LibraryIcon} iconPlacement={"left"}>
    Button
  </Button>
);

export const WithIconRightAndText = () => (
  <Button variant={"text"} icon={LibraryIcon} iconPlacement={"right"}>
    Button
  </Button>
);
