import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:eat_somewhere/widgets/desktop_mobile_refresh_indicator.dart';
import 'package:eat_somewhere/widgets/loading_widget.dart';
import 'package:flutter/material.dart';
import 'package:sticky_headers/sticky_headers.dart';

class SearchableListScreen<T> extends StatefulWidget {
  final List<T>? items;
  Function(dynamic) mappingFunction;
  String Function(dynamic) stringFunction;
  Future Function()? onRefresh;
  String query = "";
  Future Function()? newAction;
  String title;

  SearchableListScreen(
      {Key? key,
      required this.items,
      required this.mappingFunction,
      required this.stringFunction,
      required this.newAction,
      this.onRefresh,
      required this.title})
      : super(key: key);

  @override
  State<SearchableListScreen> createState() => _SearchableListScreenState<T>();
}

class _SearchableListScreenState<T> extends State<SearchableListScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(title: Text(widget.title)),
        floatingActionButton: widget.newAction == null
            ? null
            : FloatingActionButton(
                onPressed: () async {
                  await widget.newAction!();
                  setState(() {});
                },
                child: const Icon(Icons.add),
              ),
        body: ConstrainedContainer(
          child: SearchableList(
            onRefresh: widget.onRefresh,
            items: widget.items,
            mappingFunction: widget.mappingFunction,
            stringFunction: widget.stringFunction,
            query: widget.query,
          ),
        ));
    //return StickyHeader(header: , content: );
  }
}

class SearchableList<T> extends StatefulWidget {
  final List<T>? items;
  Function(dynamic) mappingFunction;
  String Function(dynamic) stringFunction;
  Future Function()? onRefresh;
  String query = "";

  SearchableList(
      {Key? key,
      required this.items,
      required this.mappingFunction,
      required this.stringFunction,
      this.query = "",
      this.onRefresh})
      : super(key: key);

  @override
  State<SearchableList> createState() => _SearchableListState<T>();
}

class _SearchableListState<T> extends State<SearchableList> {
  List<T>? filteredItems = [];

  @override
  void initState() {
    super.initState();
    filterItems(widget.query);
  }

  void filterItems(String query) {
    setState(() {
      widget.query = query;
      filteredItems = widget.items?.where((item) {
        return widget
            .stringFunction(item)
            .toLowerCase()
            .contains(query.toLowerCase());
      }).toList() as List<T>?;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Container(
          child: TextField(
            onChanged: (value) {
              filterItems(value);
            },
            decoration: InputDecoration(
              hintText: 'Search...',
              border: OutlineInputBorder(),
            ),
          ),
        ),
        Expanded(
            child: Container(
          child: filteredItems == null
              ? LoadingWidget()
              : widget.items!.isEmpty
                  ? Text("No entries")
                  : filteredItems!.isEmpty
                      ? Text("No result")
                      : DesktopMobileRefreshIndicator(
                          onRefresh: widget.onRefresh,
                          child: ListView.builder(
                              itemCount: widget.query.isEmpty
                                  ? widget.items!.length
                                  : filteredItems!.length,
                              itemBuilder: (context, index) {
                                T item = widget.query.isEmpty
                                    ? widget.items![index]
                                    : filteredItems![index];
                                Widget w = widget.mappingFunction(item);
                                return w;
                              })),
        ))
      ],
    );
  }
}
