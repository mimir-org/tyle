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
  <Button variant={"text"} leftIcon={LibraryIcon} iconOnly>
    Hidden text
  </Button>
);

export const WithIconLeftAndText = () => (
  <Button variant={"text"} leftIcon={LibraryIcon}>
    Button
  </Button>
);

export const WithIconRightAndText = () => (
  <Button variant={"text"} rightIcon={LibraryIcon}>
    Button
  </Button>
);
