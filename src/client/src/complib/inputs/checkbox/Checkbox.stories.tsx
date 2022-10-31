import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Checkbox } from "complib/inputs/checkbox/Checkbox";

export default {
  title: "Inputs/Checkbox",
  component: Checkbox,
} as ComponentMeta<typeof Checkbox>;

const Template: ComponentStory<typeof Checkbox> = (args) => <Checkbox {...args} />;

export const Default = Template.bind({});
