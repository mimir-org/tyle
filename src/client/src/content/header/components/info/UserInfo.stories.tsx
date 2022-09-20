import { ComponentStory } from "@storybook/react";
import { UserInfo } from "./UserInfo";

export default {
  title: "Content/Header/Info/UserInfo",
  component: UserInfo,
};

const Template: ComponentStory<typeof UserInfo> = (args) => <UserInfo {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Threepwood",
  permissions: ["Company A: Manage", "Company B: Manage"],
};
