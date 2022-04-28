import { ComponentMeta } from "@storybook/react";
import { Button } from "./Button";
import { LibraryIcon } from "../../assets/icons/modules";

export default {
  title: "Buttons/Outlined",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button variant={"outlined"}>Button</Button>;

export const Disabled = () => (
  <Button variant={"outlined"} disabled>
    Button
  </Button>
);

export const WithIconOnly = () => (
  <Button variant={"outlined"} leftIcon={LibraryIcon} iconOnly>
    Hidden text
  </Button>
);

export const WithIconLeftAndText = () => (
  <Button variant={"outlined"} leftIcon={LibraryIcon}>
    Button
  </Button>
);

export const WithIconRightAndText = () => (
  <Button variant={"outlined"} rightIcon={LibraryIcon}>
    Button
  </Button>
);
