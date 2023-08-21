import { Meta, StoryFn } from "@storybook/react";
import { LibraryIcon } from "@mimirorg/component-library";
import { InfoItemDescription } from "features/common/info-item/InfoItemDescription";

export default {
  title: "Features/Common/InfoItem/InfoItemDescription",
  component: InfoItemDescription,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    descriptors: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as Meta<typeof InfoItemDescription>;

const Template: StoryFn<typeof InfoItemDescription> = (args) => <InfoItemDescription {...args} />;

export const Default = Template.bind({});

export const Actionable = Template.bind({});
Actionable.args = {
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] ItemDescription.onAction"),
};
