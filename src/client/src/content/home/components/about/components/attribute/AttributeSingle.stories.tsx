import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AttributeSingle } from "./AttributeSingle";

export default {
  title: "Content/Home/About/Attribute/AttributeSingle",
  component: AttributeSingle,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
    value: "40%",
  },
} as ComponentMeta<typeof AttributeSingle>;

const Template: ComponentStory<typeof AttributeSingle> = (args) => <AttributeSingle {...args} />;

export const Default = Template.bind({});
