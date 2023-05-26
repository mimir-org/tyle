import { Meta, StoryFn } from "@storybook/react";
import { Textarea } from "complib/inputs/textarea/Textarea";

export default {
  title: "Inputs/Textarea",
  component: Textarea,
} as Meta<typeof Textarea>;

const Template: StoryFn<typeof Textarea> = (args) => <Textarea {...args} />;

export const Default = Template.bind({});
Default.args = {
  defaultValue: "Some input",
  placeholder: "placeholder",
};
