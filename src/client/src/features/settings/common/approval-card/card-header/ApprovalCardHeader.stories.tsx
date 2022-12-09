import { ComponentStory } from "@storybook/react";
import { ApprovalCardHeader } from "features/settings/common/approval-card/card-header/ApprovalCardHeader";

export default {
  title: "Features/Settings/Common/ApprovalCardHeader",
  component: ApprovalCardHeader,
};

const Template: ComponentStory<typeof ApprovalCardHeader> = (args) => <ApprovalCardHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Pump system",
};
