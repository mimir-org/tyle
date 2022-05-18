import { Divider } from "./Divider";
import { ComponentMeta, ComponentStory } from "@storybook/react";

export default {
  title: "Data display/Divider",
  component: Divider,
  args: {
    decorative: false,
  },
} as ComponentMeta<typeof Divider>;

const Template: ComponentStory<typeof Divider> = (args) => <Divider {...args} />;

export const Horizontal = Template.bind({});
Horizontal.args = {
  orientation: "horizontal",
};

export const Vertical = Template.bind({});
Vertical.args = {
  orientation: "vertical",
};
