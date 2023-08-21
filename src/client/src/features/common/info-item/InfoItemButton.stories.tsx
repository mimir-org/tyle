import { Meta, StoryFn } from "@storybook/react";
import { LibraryIcon } from "@mimirorg/component-library";
import { InfoItemButton } from "features/common/info-item/InfoItemButton";

export default {
  title: "Features/Common/InfoItem/InfoItemButton",
  component: InfoItemButton,
} as Meta<typeof InfoItemButton>;

const Template: StoryFn<typeof InfoItemButton> = (args) => <InfoItemButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Pressure, absolute",
  color: "orange",
  descriptors: {
    condition: "Maximum",
    qualifier: "Operating",
    source: "Calculated",
  },
};

export const Actionable = Template.bind({});
Actionable.args = {
  ...Default.args,
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] ItemInfoButton.onAction"),
};
