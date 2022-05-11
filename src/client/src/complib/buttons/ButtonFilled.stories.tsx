import { ComponentMeta } from "@storybook/react";
import { Button } from "./Button";
import { LibraryIcon } from "../../assets/icons/modules";

export default {
  title: "Buttons/Filled",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button>Button</Button>;

export const Disabled = () => <Button disabled>Button</Button>;

export const WithIconOnly = () => (
  <Button icon={LibraryIcon} iconOnly>
    Hidden text
  </Button>
);

export const WithIconLeftAndText = () => (
  <Button icon={LibraryIcon} iconPlacement={"left"}>
    Button
  </Button>
);

export const WithIconRightAndText = () => (
  <Button icon={LibraryIcon} iconPlacement={"right"}>
    Button
  </Button>
);
