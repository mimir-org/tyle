import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeDescription } from "./AttributeDescription";

export default {
  title: "Content/Home/About/Attribute/AttributeDescription",
  component: AttributeDescription,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as ComponentMeta<typeof AttributeDescription>;

const Template: ComponentStory<typeof AttributeDescription> = (args) => <AttributeDescription {...args} />;

export const Default = Template.bind({});
