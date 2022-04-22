import { Divider } from "./Divider";
import { ComponentMeta, ComponentStory } from "@storybook/react";

export default {
  title: "Data display/Divider",
  component: Divider,
} as ComponentMeta<typeof Divider>;

const Template: ComponentStory<typeof Divider> = (args) => <Divider {...args} />;

export const Horizontal = Template.bind({});
Horizontal.args = {
  variant: "horizontal",
};

export const Vertical = Template.bind({});
Vertical.args = {
  variant: "vertical",
};
