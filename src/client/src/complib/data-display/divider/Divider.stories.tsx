import { Meta, StoryFn } from "@storybook/react";
import { Divider } from "complib/data-display/divider/Divider";

export default {
  title: "Data display/Divider",
  component: Divider,
  args: {
    decorative: false,
  },
} as Meta<typeof Divider>;

const Template: StoryFn<typeof Divider> = (args) => <Divider {...args} />;

export const Horizontal = Template.bind({});
Horizontal.args = {
  orientation: "horizontal",
};

export const Vertical = Template.bind({});
Vertical.args = {
  orientation: "vertical",
};
