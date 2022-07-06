import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Box } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { HomeSection } from "./HomeSection";
import { ItemListProps } from "./search/components/item/ItemList";
import { Default as ItemList } from "./search/components/item/ItemList.stories";
import { SearchBarProps } from "./search/components/SearchBar";
import { Default as SearchBar } from "./search/components/SearchBar.stories";

export default {
  title: "Content/Home/HomeSection",
  component: HomeSection,
} as ComponentMeta<typeof HomeSection>;

const Template: ComponentStory<typeof HomeSection> = (args) => <HomeSection {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Section",
  children: "Simple text content in section",
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  title: "Section",
  children: (
    <Box display={"flex"} flexDirection={"column"} gap={"12px"} width={"500px"}>
      <Text variant={"title-large"}>Custom content</Text>
      <SearchBar {...(SearchBar.args as SearchBarProps)} />
      <ItemList {...(ItemList.args as ItemListProps)} />
    </Box>
  ),
};
