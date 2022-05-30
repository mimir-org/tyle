import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeCheckbox } from "./AttributeCheckbox";

export default {
  title: "Content/Home/About/Attribute/AttributeCheckbox",
  component: AttributeCheckbox,
  args: {
    color: "orange",
    children: "Attribute",
  },
} as ComponentMeta<typeof AttributeCheckbox>;

const Template: ComponentStory<typeof AttributeCheckbox> = (args) => <AttributeCheckbox {...args} />;

export const Default = Template.bind({});

export const Checked = Template.bind({});
Checked.args = {
  checked: true,
};

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};
