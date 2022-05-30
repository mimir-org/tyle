import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeInfoCheckbox } from "./AttributeInfoCheckbox";

export default {
  title: "Content/Home/About/Attribute/AttributeInfoCheckbox",
  component: AttributeInfoCheckbox,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
    checked: false,
  },
} as ComponentMeta<typeof AttributeInfoCheckbox>;

const Template: ComponentStory<typeof AttributeInfoCheckbox> = (args) => <AttributeInfoCheckbox {...args} />;

export const Default = Template.bind({});
