import { StoryFn } from "@storybook/react";
import { PermissionCardHeader } from "features/settings/common/permission-card/card-header/PermissionCardHeader";

export default {
  title: "Features/Settings/Common/PermissionCardHeader",
  component: PermissionCardHeader,
};

const Template: StoryFn<typeof PermissionCardHeader> = (args) => <PermissionCardHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Jane Smith",
};
