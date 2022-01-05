# AddressableAsset

Unity の Addressable Asset が異様に使いづらいのでサンプルを作成してみました。


# Addressables Groups
## Addressables Group とは？

Addressables Group とはアセットを特定の設定値を割り当てるグループです。
初見ではファイルを纏めるグループのように思えますが、実際には後述する設定値によってファイルのまとまり方が変わります。

## 設定値

基本的には設定を変更した場合はクリーンビルドをすることをオススメします。

設定がキャッシュされている場合があり、Addressables Groups の `Clean Build` をしても設定が更新されない場合があります。  
その場合は AddressableAssetsData ディレクトリを削除して設定し直しという選択肢もありです。

### `Content Update Restriction`
#### `Update Restriction`

|設定値|説明|
|:--|:--|
|Cannot Change Post Release||
|Can Change Post Release||

### `Content Packing & Loading`
#### `Asset Bundle Compression`
出力するアセットバンドルの圧縮モードを選択します。

[Asset Bundle Compression](https://docs.unity3d.com/2021.2/Documentation/Manual/AssetBundles-Cache.html)

|設定値|説明|
|:--|:--|
|Uncompressed|無圧縮|
|LZ4|圧縮率は低い、解凍が早い|
|LZMA|圧縮率が高い、解凍が遅い（デフォルト）|

選択基準としてはネットワークからダウンロードする場合にファイルサイズが大きいとダウンロードに時間がかかるため LZMA を選択するなどケース・バイ・ケースとなります。  
一応、Addressables Asset の公式サイトには LZ4 が通常は最も効率的なオプションと記載はあります。

#### `Include in Build`

グループ内のアセットをコンテンツビルドの際に含めるかどうか。これにチェックが入っていないとコンテンツビルドの際には含まれないことになる。
（個人的にはこのオプションは不要だと思うが…）  
否応なく ON で良いと思う。

#### `Use Asset Bundle Cahce`

リモートで配布されているアセットをキャッシュするかどうか。  
毎回ダウンロードさせたいのであれば OFF にする。

#### `Asset Bundle CRC`

アセットを読み込む前にアセットの整合性を検証するかどうか。
|設定値|説明|
|:--|:--|
|Disabled|整合性の検証をしない|
|Enabled, Including Cached|キャッシュを含め、常に整合性の検証を行う|
|Enabled, Excluding Cached|キャッシュを除く、ダウンロー時のみ整合性の検証を行う|

#### `Use UnityWebRequest for Local Asset Bundles`

ローカルのアセットをロードする際に AssetBundle.LoadFromFileAsync の代わりに UnityWebRequestAssetBundle.GetAssetBundle を使用するかどうか。  
このフラグを ON にするメリットが良く分からない。ライブラリ開発者が利用するだけと思われる。

#### `Include Addresses in Catalog`

アドレス文字列をカタログに含めるかどうか。
グループ内のアセットをアドレス文字列を使用して読み込まない場合はこれを OFF にすることでカタログサイズを小さくすることが可能。

#### `Include GUIDs in Catalog`

GUID 文字列をカタログに含めるかどうか。
グループ内のアセットを GUID を使用して読み込まない場合はこれを OFF にすることでカタログサイズを小さくすることが可能。

#### `Include Labels in Catalog`

ラベル文字列をカタログに含めるかどうか。
グループ内のアセットをラベルを使用して読み込まない場合はこれを OFF にすることでカタログサイズを小さくすることが可能。

#### `Internal Asset Naming Mode`

カタログ内のアセットに内部で付ける名前のルール。

|設定値|説明|
|:--|:--|
|Full Path|プロジェクト内のアセットのフルパス|
|Filename|アセットのファイル名|
|GUID|アセットの GUID|
|Dynamic|グループ内のアセットに基づいて Addressables が最小の名称を選択|

これは Dynamic で十分。

#### `Bundle Mode`

アセットをまとめる単位を指定します。

|設定値|説明|
|:--|:--|
|Pack Together|グループに登録されているファイルを一つのファイルに結合して出力します。|
|Pack Separately|グループに登録されているファイルを個別に出力します。|
|Pack Together By Label|グループに登録されているファイルをラベルを単位で一つのファイルに結合して出力します。|