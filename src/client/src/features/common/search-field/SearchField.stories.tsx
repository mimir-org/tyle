import { ComponentStory } from "@storybook/react";
import { SearchField } from "features/common/search-field/SearchField";

export default {
  title: "Features/Common/SearchField",
  component: SearchField,
};

const Template: ComponentStory<typeof SearchField> = (args) => <SearchField {...args} />;

export const Default = Template.bind({});
