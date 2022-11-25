import { ComponentStory } from "@storybook/react";
import { UserList } from "features/settings/permission/user-list/UserList";
import { UserListItem, UserListItemProps } from "features/settings/permission/user-list/UserListItem";
import { Default as UserListItemStory } from "features/settings/permission/user-list/UserListItem.stories";

export default {
  title: "Settings/Permission/UserList",
  component: UserList,
  subcomponents: { UserListItem },
};

const Template: ComponentStory<typeof UserList> = (args) => <UserList {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Users",
  children: [...Array(5)].map((_, i) => (
    <UserListItemStory key={i} {...(UserListItemStory.args as UserListItemProps)} />
  )),
};
