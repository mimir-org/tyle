import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeInfoButton } from "./AttributeInfoButton";

export default {
  title: "Content/Home/About/Attribute/AttributeInfoButton",
  component: AttributeInfoButton,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as ComponentMeta<typeof AttributeInfoButton>;

const Template: ComponentStory<typeof AttributeInfoButton> = (args) => <AttributeInfoButton {...args} />;

export const Default = Template.bind({});
