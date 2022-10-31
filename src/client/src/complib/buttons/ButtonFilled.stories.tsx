import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Button } from "complib/buttons/Button";

export default {
  title: "Buttons/Filled",
  component: Button,
  args: {
    children: "Button",
    variant: "filled",
    disabled: false,
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
