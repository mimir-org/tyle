import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeButton } from "./AttributeButton";

export default {
  title: "Content/Home/About/Attribute/AttributeButton",
  component: AttributeButton,
  args: {
    color: "orange",
    variant: "large",
    children: "Attribute",
  },
} as ComponentMeta<typeof AttributeButton>;

const Template: ComponentStory<typeof AttributeButton> = (args) => <AttributeButton {...args} />;

export const Default = Template.bind({});

export const VariantLarge = Template.bind({});
VariantLarge.args = {
  variant: "large",
};

export const VariantMedium = Template.bind({});
VariantMedium.args = {
  variant: "medium",
};

export const Disabled = Template.bind({});
Disabled.args = {
  disabled: true,
};
