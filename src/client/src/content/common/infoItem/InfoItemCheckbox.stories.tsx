import { ComponentMeta, ComponentStory } from "@storybook/react";
import { InfoItemCheckbox } from "./InfoItemCheckbox";

export default {
  title: "Content/Common/InfoItem/InfoItemCheckbox",
  component: InfoItemCheckbox,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    descriptors: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
    checked: false,
  },
} as ComponentMeta<typeof InfoItemCheckbox>;

const Template: ComponentStory<typeof InfoItemCheckbox> = (args) => <InfoItemCheckbox {...args} />;

export const Default = Template.bind({});
