import { Meta, StoryFn } from "@storybook/react";
import { InfoItemCheckbox } from "features/common/info-item/InfoItemCheckbox";

export default {
  title: "Features/Common/InfoItem/InfoItemCheckbox",
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
} as Meta<typeof InfoItemCheckbox>;

const Template: StoryFn<typeof InfoItemCheckbox> = (args) => <InfoItemCheckbox {...args} />;

export const Default = Template.bind({});
