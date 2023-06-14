import { Meta, StoryFn } from "@storybook/react";
import { Checkbox } from "complib/inputs/checkbox/Checkbox";

export default {
  title: "Inputs/Checkbox",
  component: Checkbox,
} as Meta<typeof Checkbox>;

const Template: StoryFn<typeof Checkbox> = (args) => <Checkbox {...args} />;

export const Default = Template.bind({});
