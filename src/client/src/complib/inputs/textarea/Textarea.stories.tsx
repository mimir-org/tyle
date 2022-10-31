import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Textarea } from "complib/inputs/textarea/Textarea";

export default {
  title: "Inputs/Textarea",
  component: Textarea,
} as ComponentMeta<typeof Textarea>;

const Template: ComponentStory<typeof Textarea> = (args) => <Textarea {...args} />;

export const Default = Template.bind({});
Default.args = {
  defaultValue: "Some input",
  placeholder: "placeholder",
};
