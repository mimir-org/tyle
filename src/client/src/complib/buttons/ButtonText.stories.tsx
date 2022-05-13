import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "./Button";
import { LibraryIcon } from "../../assets/icons/modules";

export default {
  title: "Buttons/Text",
  component: Button,
  args: {
    children: "Button",
    variant: "text",
  },
} as ComponentMeta<typeof Button>;

const Template: ComponentStory<typeof Button> = (args) => <Button {...args} />;

export const Default = Template.bind({});

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};

export const WithIconOnly = Template.bind({});
WithIconOnly.args = {
  icon: LibraryIcon,
  iconOnly: true,
};

export const WithIconLeftAndText = Template.bind({});
WithIconLeftAndText.args = {
  icon: LibraryIcon,
  iconPlacement: "left",
};

export const WithIconRightAndText = Template.bind({});
WithIconRightAndText.args = {
  icon: LibraryIcon,
  iconPlacement: "right",
};

export const WithDanger = Template.bind({});
WithDanger.args = {
  danger: true,
};
