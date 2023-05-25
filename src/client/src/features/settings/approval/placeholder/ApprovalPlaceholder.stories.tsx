import { StoryFn } from "@storybook/react";
import { ApprovalPlaceholder } from "features/settings/approval/placeholder/ApprovalPlaceholder";

export default {
  title: "Features/Settings/Approval/ApprovalPlaceholder",
  component: ApprovalPlaceholder,
};

const Template: StoryFn<typeof ApprovalPlaceholder> = (args) => <ApprovalPlaceholder {...args}></ApprovalPlaceholder>;

export const Default = Template.bind({});
Default.args = {
  text: "There is no types for approval",
};
