import { ComponentMeta } from "@storybook/react";
import { Button } from "./Button";
import { LibraryIcon } from "../../assets/icons/modules";

export default {
  title: "Buttons/Button",
  component: Button,
} as ComponentMeta<typeof Button>;

export const Default = () => <Button>Button</Button>;

export const Disabled = () => <Button disabled>Button</Button>;

export const AsAnchor = () => <Button as={"a"}>Button</Button>;

export const WithIconOnly = () => (
  <Button leftIcon={LibraryIcon} iconOnly>
    Hidden text
  </Button>
);

export const WithIconLeftAndText = () => <Button leftIcon={LibraryIcon}>Button</Button>;

export const WithIconRightAndText = () => <Button rightIcon={LibraryIcon}>Button</Button>;
